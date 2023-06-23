package com.example.voiceacademyapp.service.services;

import android.content.Context;
import android.content.SharedPreferences;


import androidx.annotation.NonNull;

import com.example.voiceacademyapp.DTOs.EditUserDTO;
import com.example.voiceacademyapp.DTOs.UserDTO;
import com.example.voiceacademyapp.api.responds.GenericResponse;
import com.example.voiceacademyapp.api.responds.LoginResponse;
import com.example.voiceacademyapp.model.User;
import com.example.voiceacademyapp.service.client.UserClient;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class UserService {

    private Context context;
    private static String token;
    private static int idUser;
    private String role;

    public UserService(Context context) {
        this.context = context;
        SharedPreferences sharedPreferences = context.getSharedPreferences("SharedPreferences", Context.MODE_PRIVATE);
        token = sharedPreferences.getString("token", "");
        idUser = sharedPreferences.getInt("id", 0);
        role = sharedPreferences.getString("role", "");
    }



    public static void searchUserById(int idUser, UserServiceCallback callback) {

        Call<GenericResponse<User>> call = UserClient.getInstance().getApiService().getUser("Bearer " + token, idUser);
        call.enqueue(new Callback<GenericResponse<User>>() {
            @Override
            public void onResponse(@NonNull Call<GenericResponse<User>> call, @NonNull retrofit2.Response<GenericResponse<User>> response) {
                if (response.isSuccessful()) {
                    if (response.isSuccessful()) {
                        callback.onSuccess(response.body());
                    } else {
                        callback.onSuccess(new GenericResponse<User>(response.code()));
                    }
                } else {
                    callback.onSuccess(new GenericResponse<User>(response.code()));
                }
            }

            @Override
            public void onFailure(@NonNull Call<GenericResponse<User>> call, @NonNull Throwable throwable) {
                callback.onFailure(throwable);
            }
        });
    }

    public static void updateUser(EditUserDTO user, UserServiceCallback callback){
        user.setIdUser(idUser);
        Call<GenericResponse<User>> call = UserClient.getInstance().getApiService().updateUser("Bearer " + token, user);
        call.enqueue(new Callback<GenericResponse<User>>() {
            @Override
            public void onResponse(Call<GenericResponse<User>> call, Response<GenericResponse<User>> response) {
                if (response.isSuccessful()) {
                    if (response.isSuccessful()) {
                        callback.onSuccess(response.body());
                    } else {
                        callback.onSuccess(new GenericResponse<User>(response.code()));
                    }
                } else {
                    callback.onSuccess(new GenericResponse<User>(response.code()));
                }
            }

            @Override
            public void onFailure(@NonNull Call<GenericResponse<User>> call, @NonNull Throwable throwable) {
                callback.onFailure(throwable);
            }
        });
    }

    public interface UserServiceCallback {

        void onSuccess(GenericResponse<User> response);

        void onFailure(Throwable throwable);

    }

    }


