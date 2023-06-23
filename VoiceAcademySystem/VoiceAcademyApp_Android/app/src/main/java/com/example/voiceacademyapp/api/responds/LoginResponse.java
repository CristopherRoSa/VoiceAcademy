package com.example.voiceacademyapp.api.responds;

import com.example.voiceacademyapp.DTOs.TokenDTO;

public class LoginResponse {
    private int statusCode;
    private boolean correct;
    private String message;
    private TokenDTO token;

    public LoginResponse() {

    }
    public LoginResponse(int statusCode) {
        statusCode = statusCode;
    }
    public int getStatusCode() {
        return statusCode;
    }

    public void setStatusCode(int statusCode) {
        statusCode = statusCode;
    }

    public boolean isCorrect() {
        return correct;
    }

    public void setCorrect(boolean correct) {
        this.correct = correct;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }

    public TokenDTO getToken() {
        return token;
    }

    public void setToken(TokenDTO token) {
        this.token = token;
    }
}
