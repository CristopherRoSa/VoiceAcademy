package com.example.voiceacademyapp.service.services;


import android.content.Context;
import android.content.SharedPreferences;


import androidx.annotation.NonNull;

import com.example.voiceacademyapp.DTOs.LoginDTO;
import com.example.voiceacademyapp.DTOs.UserDTO;
import com.example.voiceacademyapp.api.responds.LoginResponse;
import com.example.voiceacademyapp.service.client.LoginClient;

import retrofit2.Call;
import retrofit2.Callback;

public class LoginService {

    private final Context context;
    private final String token;
    public LoginService(Context context) {
        this.context = context;
        SharedPreferences sharedPreferences = context.getSharedPreferences("SharedPreferences", Context.MODE_PRIVATE);
        token = sharedPreferences.getString("token", "");
    }
    public static void login(LoginDTO user, LoginServiceCallback callback) {

        Call<LoginResponse> call = LoginClient.getInstance().getApiService().login(user);
        call.enqueue(new Callback<LoginResponse>() {
            @Override
            public void onResponse(@NonNull Call<LoginResponse> call, @NonNull retrofit2.Response<LoginResponse> response) {
                if (response.isSuccessful()) {
                    if (response.isSuccessful()) {
                        callback.onSuccess(response.body());
                    } else {
                        callback.onSuccess(new LoginResponse(response.code()));
                    }
                } else {
                    callback.onSuccess(new LoginResponse(response.code()));
                }
            }

            @Override
            public void onFailure(@NonNull Call<LoginResponse> call, @NonNull Throwable throwable) {
                callback.onFailure(throwable);
            }
        });

    }
    public static void addUser(UserDTO user, LoginServiceCallback callback){
        Call<LoginResponse> call = LoginClient.getInstance().getApiService().addUser(user);
        call.enqueue(new Callback<LoginResponse>() {
            @Override
            public void onResponse(@NonNull Call<LoginResponse> call, @NonNull retrofit2.Response<LoginResponse> response) {
                if (response.isSuccessful()) {
                    if (response.isSuccessful()) {
                        callback.onSuccess(response.body());
                    } else {
                        callback.onSuccess(new LoginResponse(response.code()));
                    }
                } else {
                    callback.onSuccess(new LoginResponse(response.code()));
                }
            }

            @Override
            public void onFailure(@NonNull Call<LoginResponse> call, @NonNull Throwable throwable) {
                callback.onFailure(throwable);
            }
        });
    }
    public interface LoginServiceCallback {

        void onSuccess(LoginResponse response);

        void onFailure(Throwable throwable);

    }

}
