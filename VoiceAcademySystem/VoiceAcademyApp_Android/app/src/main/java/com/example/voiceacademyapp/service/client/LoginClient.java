package com.example.voiceacademyapp.service.client;


import com.example.voiceacademyapp.api.LoginService;
import com.example.voiceacademyapp.api.configuration.ConfigApi;

import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class LoginClient {
    private final LoginService apiService;
    private static LoginClient instance = null;

    public LoginClient() {
        Retrofit retrofit = new Retrofit.Builder().baseUrl(ConfigApi.getBaseUrl())
                .addConverterFactory(GsonConverterFactory.create())
                .build();
        apiService = retrofit.create(LoginService.class);
    }

    public static synchronized LoginClient getInstance() {
        if (instance == null) {
            instance = new LoginClient();
        }
        return instance;
    }

    public LoginService getApiService() {
        return apiService;
    }

}
