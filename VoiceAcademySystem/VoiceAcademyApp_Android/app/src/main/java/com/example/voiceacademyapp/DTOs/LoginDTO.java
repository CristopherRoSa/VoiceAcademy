package com.example.voiceacademyapp.DTOs;

import androidx.lifecycle.MutableLiveData;

public class LoginDTO {
    private String User;
    private String Password;

    public LoginDTO(String username, String password) {
        this.User = username;
        this.Password = password;
    }

    public String getPassword() {
        return Password;
    }
    public void setPassword(String password) {
        this.Password = password;
    }
    public String getUsername() {
        return User;
    }

    public void setUsername(String username) {
        this.User = username;
    }
}
