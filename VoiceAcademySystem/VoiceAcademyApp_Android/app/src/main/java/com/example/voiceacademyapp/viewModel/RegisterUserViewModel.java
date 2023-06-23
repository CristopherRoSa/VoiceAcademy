package com.example.voiceacademyapp.viewModel;

import android.app.Application;
import android.content.Context;
import android.content.Intent;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.lifecycle.AndroidViewModel;
import androidx.lifecycle.MutableLiveData;

import com.example.voiceacademyapp.DTOs.LoginDTO;
import com.example.voiceacademyapp.DTOs.UserDTO;
import com.example.voiceacademyapp.R;

import com.example.voiceacademyapp.activity.LoginActivity;
import com.example.voiceacademyapp.activity.MenuActivity;

import com.example.voiceacademyapp.api.responds.GenericResponse;
import com.example.voiceacademyapp.api.responds.LoginResponse;
import com.example.voiceacademyapp.service.services.LoginService;
import com.example.voiceacademyapp.service.services.UserService;

import java.net.HttpURLConnection;
import java.nio.charset.StandardCharsets;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.regex.Pattern;

import retrofit2.Call;

public class RegisterUserViewModel extends AndroidViewModel {
    private final Context context;
    private final LoginService loginService;
    private final MutableLiveData<String> fistName = new MutableLiveData<>();
    private final MutableLiveData<String> lastName = new MutableLiveData<>();
    private final MutableLiveData<Integer> age = new MutableLiveData<>();
    private final MutableLiveData<String> email = new MutableLiveData<>();
    private final MutableLiveData<String> password = new MutableLiveData<>();
    public MutableLiveData<String> getPassword() {
        return password;
    }
    public MutableLiveData<String> getFistName() {
        return fistName;
    }
    public MutableLiveData<String> getLastName() {
        return lastName;
    }
    private final MutableLiveData<String> ageString = new MutableLiveData<>();
    public MutableLiveData<Integer> getAge() {
        return age;
    }
    public MutableLiveData<String> getEmail() {
        return email;
    }

    public RegisterUserViewModel(@NonNull Application application) {
        super(application);
        context = application.getApplicationContext();
        loginService = new LoginService(context);


    }
    public void onBtnSaveCliked() {

        if (fistName.getValue() != null & lastName.getValue() != null & age.getValue() != null & email.getValue() != null & password.getValue() != null) {
            if(isValidEmail(email.getValue())) {

                String firstName = this.fistName.getValue();
                String lastName = this.lastName.getValue();
                Integer age = this.age.getValue();
                String email = this.email.getValue();
                String password = this.password.getValue();
                password = computeSHA256Hash(password);
                UserDTO user = new UserDTO(0, email, age, firstName, lastName, password, null);
                addUser(user);
            }
            else{
                Toast.makeText(context, R.string.invalid_email_label, Toast.LENGTH_SHORT).show();
            }
        } else {
            Toast.makeText(context, R.string.empty_fields_label, Toast.LENGTH_SHORT).show();
        }
    }

    private void addUser(UserDTO user){

        LoginService.addUser(user, new LoginService.LoginServiceCallback() {
            @Override
            public void onSuccess(LoginResponse response) {
                int code = response.getStatusCode();
                if (response != null) {
                    if (code == HttpURLConnection.HTTP_FORBIDDEN) {
                        Toast.makeText(context, R.string.expired_session_label, Toast.LENGTH_SHORT).show();
                    } else if (code == HttpURLConnection.HTTP_NOT_FOUND || code == HttpURLConnection.HTTP_BAD_REQUEST) {
                        Toast.makeText(context, R.string.user_not_added_label, Toast.LENGTH_SHORT).show();
                    }else{
                        Toast.makeText(context,R.string.user_added_label, Toast.LENGTH_SHORT).show();
                        //TODO: go to login page
                        goToLogin();
                    }
                } else {
                    Toast.makeText(context, R.string.user_not_added_label, Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(Throwable throwable) {
                Toast.makeText(context, R.string.service_not_available_label, Toast.LENGTH_SHORT).show();
            }

        });
    }
    public void onBtnCancelClicked() {
        goToLogin();
    }
    private void goToLogin(){
        Intent intent = new Intent(context, LoginActivity.class);
        intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TASK);
        context.startActivity(intent);
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

    private static final String EMAIL_REGEX =
            "^[A-Za-z0-9+_.-]+@[A-Za-z0-9.-]+$";

    public static boolean isValidEmail(String email) {
        Pattern pattern = Pattern.compile(EMAIL_REGEX);
        return pattern.matcher(email).matches();
    }

}

