package com.example.voiceacademyapp.viewModel;

import android.app.Application;
import android.content.Context;
import android.content.Intent;

import androidx.annotation.NonNull;
import androidx.lifecycle.AndroidViewModel;

import com.example.voiceacademyapp.DTOs.TokenDTO;
import com.example.voiceacademyapp.activity.AccountActivity;
import com.example.voiceacademyapp.activity.LoginActivity;
import com.example.voiceacademyapp.activity.PodcastsActivity;

public class MenuViewModel extends AndroidViewModel {
    private final Context context;
    private TokenDTO user;

    public MenuViewModel(@NonNull Application application) {
        super(application);
        context = application.getApplicationContext();
    }
    public void setUser(TokenDTO user) {
        this.user = user;
    }


    public void onLogoutButtonClicked() {
        Intent intent = new Intent(context, LoginActivity.class);
        intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TASK);
        context.startActivity(intent);
    }
    public void onProfileButtonClicked() {}
    public void onSearchPodcastButtonClicked() {
        Intent intent = new Intent(context, PodcastsActivity.class);
        intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TASK);
        context.startActivity(intent);
    }
    public void onViewAccountButtonClicked() {
        Intent intent = new Intent(context, AccountActivity.class);
        intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TASK);
        context.startActivity(intent);
    }
}
