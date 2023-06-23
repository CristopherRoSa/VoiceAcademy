package com.example.voiceacademyapp.viewModel;

import android.app.Application;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.lifecycle.AndroidViewModel;
import androidx.lifecycle.MutableLiveData;

import com.example.voiceacademyapp.DTOs.LoginDTO;
import com.example.voiceacademyapp.DTOs.TokenDTO;
import com.example.voiceacademyapp.R;
import com.example.voiceacademyapp.activity.MenuActivity;
import com.example.voiceacademyapp.activity.RegisterUserActivity;
import com.example.voiceacademyapp.api.responds.GenericResponse;
import com.example.voiceacademyapp.api.responds.LoginResponse;
import com.example.voiceacademyapp.service.services.LoginService;

import java.net.HttpURLConnection;
import java.nio.charset.StandardCharsets;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

public class LoginViewModel extends AndroidViewModel {

    private final MutableLiveData<String> username = new MutableLiveData<>();
    private final MutableLiveData<String> password = new MutableLiveData<>();
    private final Context context;

    public MutableLiveData<String> getPassword() {
        return password;
    }
    public MutableLiveData<String> getUsername() {
        return username;
    }

    private final LoginService loginService;

    public LoginViewModel(@NonNull Application application) {
        super(application);
        context = application.getApplicationContext();
        loginService = new LoginService(context);
    }

    public void onLoginButtonClicked() {
        if (username.getValue() != null & password.getValue() != null) {
            String username = this.username.getValue();
            String password = this.password.getValue();
            password = computeSHA256Hash(password);
            LoginDTO user = new LoginDTO(username, password);
            login(user);
        } else {
            Toast.makeText(context, R.string.empty_fields_label, Toast.LENGTH_SHORT).show();
        }
    }

    private void login(LoginDTO user) {
        LoginService.login(user, new LoginService.LoginServiceCallback(){
            @Override
            public void onSuccess(LoginResponse response) {
                int code = response.getStatusCode();
                if (code == HttpURLConnection.HTTP_FORBIDDEN) {
                    Toast.makeText(context, R.string.expired_session_label, Toast.LENGTH_SHORT).show();
                } else if (code == HttpURLConnection.HTTP_NOT_FOUND || code == HttpURLConnection.HTTP_BAD_REQUEST){
                    Toast.makeText(context, R.string.invalid_data_label, Toast.LENGTH_SHORT).show();
                } else {
                    TokenDTO token = (TokenDTO) response.getToken();
                    setToken(token);
                    Toast.makeText(context, R.string.login_successful_label, Toast.LENGTH_SHORT).show();
                    goToMainMenu();
                }
            }

            @Override
            public void onFailure(Throwable throwable) {
                Toast.makeText(context, R.string.service_not_available_label, Toast.LENGTH_SHORT).show();
            }
        });
    }

    public static String computeSHA256Hash(String password) {
        try {
            MessageDigest digest = MessageDigest.getInstance("SHA-256");
            byte[] bytes = digest.digest(password.getBytes(StandardCharsets.UTF_8));
            StringBuilder stringBuilder = new StringBuilder();
            for (byte item : bytes) {
                String hex = String.format("%02x", item);
                stringBuilder.append(hex);
            }
            return stringBuilder.toString();
        } catch (NoSuchAlgorithmException e) {
            return null;
        }
    }

    private void setToken(@NonNull TokenDTO token) {
        SharedPreferences sharedPreferences = context.getSharedPreferences("SharedPreferences", Context.MODE_PRIVATE);
        SharedPreferences.Editor editor = sharedPreferences.edit();
        editor.putString("token", token.getToken());
        editor.putString("username", token.getFullName());
        editor.putString("role", token.getRole());
        editor.putInt("id", token.getIdUser());
        editor.apply();
    }
    private void goToMainMenu() {
        Intent intent = new Intent(context, MenuActivity.class);
        intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TASK);
        context.startActivity(intent);
    }

    public void onRegisterButtonClicked(){
        Intent intent = new Intent(context, RegisterUserActivity.class);
        intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TASK);
        context.startActivity(intent);
    }
}
