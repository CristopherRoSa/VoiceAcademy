package com.example.voiceacademyapp.api;

import com.example.voiceacademyapp.DTOs.LoginDTO;
import com.example.voiceacademyapp.DTOs.UserDTO;
import com.example.voiceacademyapp.api.responds.GenericResponse;
import com.example.voiceacademyapp.api.responds.LoginResponse;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

public interface LoginService {
    @POST("/api/Login/AuthorizeUser")
    Call<LoginResponse> login(@Body LoginDTO login);
    @POST("/api/Authorization/AddUser")
    Call<LoginResponse> addUser(@Body UserDTO user);


}
