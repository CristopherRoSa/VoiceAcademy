package com.example.voiceacademyapp.activity;

import androidx.appcompat.app.AppCompatActivity;
import androidx.databinding.DataBindingUtil;
import androidx.lifecycle.ViewModelProvider;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.util.Base64;
import android.widget.ImageView;
import android.widget.Toast;

import com.example.voiceacademyapp.DTOs.TokenDTO;
import com.example.voiceacademyapp.R;
import com.example.voiceacademyapp.api.responds.GenericResponse;
import com.example.voiceacademyapp.databinding.ActivityAccountBinding;
import com.example.voiceacademyapp.databinding.ActivityLoginBinding;
import com.example.voiceacademyapp.model.User;
import com.example.voiceacademyapp.service.services.UserService;
import com.example.voiceacademyapp.viewModel.AccountViewModel;
import com.example.voiceacademyapp.viewModel.LoginViewModel;

import java.net.HttpURLConnection;

public class AccountActivity extends AppCompatActivity {
    private AccountViewModel profileViewModel;
    private ActivityAccountBinding binding;
    private UserService userService;
    private int idUser;
    private Context context;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        userService = new UserService(this);
        binding = DataBindingUtil.setContentView(this, R.layout.activity_account);
        profileViewModel = new ViewModelProvider(this).get(AccountViewModel.class);
        binding.setLifecycleOwner(this);
        binding.setAccountViewModel(profileViewModel);
        this.context = AccountActivity.this;
        loadProfileData();
    }
    @SuppressLint("ResourceType")
    private void loadProfileData() {
        SharedPreferences sharedPreferences = this.getSharedPreferences("SharedPreferences", Context.MODE_PRIVATE);
        idUser = sharedPreferences.getInt("id", 0);
        UserService.searchUserById(idUser, new UserService.UserServiceCallback() {
            @Override
            public void onSuccess(GenericResponse response) {
                int code = response.getCode();
                if (code == HttpURLConnection.HTTP_FORBIDDEN) {
                    Toast.makeText(context, R.string.expired_session_label, Toast.LENGTH_SHORT).show();
                } else if (code == HttpURLConnection.HTTP_NOT_FOUND || code == HttpURLConnection.HTTP_BAD_REQUEST){
                    Toast.makeText(context, R.string.invalid_data_label, Toast.LENGTH_SHORT).show();
                } else {
                    User user = (User) response.getData();
                    profileViewModel.getName().setValue(user.getName());
                    profileViewModel.getLastName().setValue(user.getLastName());
                    profileViewModel.getAge().setValue(user.getAge());
                    profileViewModel.getEmail().setValue(user.getEmail());
                    if(user.getImageUser() != null) {
                        String imageString = user.getImageUser(); // Obtener la cadena de imagen del objeto User
                        byte[] imageBytes = Base64.decode(imageString, Base64.DEFAULT); // Convertir la cadena en un arreglo de bytes
                        Bitmap profileImageBitmap = BitmapFactory.decodeByteArray(imageBytes, 0, imageBytes.length); // Crear el Bitmap
                        profileViewModel.getImageUser().setValue(profileImageBitmap); // Asignar el Bitmap al ImageView
                    }
                    Toast.makeText(context, R.string.login_successful_label, Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(Throwable throwable) {
                Toast.makeText(context, R.string.expired_session_label, Toast.LENGTH_SHORT).show();

            }
        });


    }


}