package com.example.voiceacademyapp.api;

import com.example.voiceacademyapp.DTOs.EditUserDTO;
import com.example.voiceacademyapp.DTOs.NewUvComunityDTO;
import com.example.voiceacademyapp.DTOs.ProfileDTO;
import com.example.voiceacademyapp.DTOs.UserDTO;
import com.example.voiceacademyapp.api.responds.GenericResponse;
import com.example.voiceacademyapp.model.User;
import com.example.voiceacademyapp.model.UvComunity;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;

public interface UserService {

    @GET("/api/user/SearchUserById/{idUser}")
    Call<GenericResponse<User>>getUser(@Header("Authorization") String token,  @Path("idUser") int idUser);
    @PUT("/api/user/UpdateUser")
    Call<GenericResponse<User>>updateUser(@Header("Authorization") String token, @Body EditUserDTO user);


}
