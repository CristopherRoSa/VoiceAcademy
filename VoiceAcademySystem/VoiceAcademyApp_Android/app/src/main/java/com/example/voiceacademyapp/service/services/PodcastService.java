package com.example.voiceacademyapp.service.services;

import android.content.Context;
import android.content.SharedPreferences;

import androidx.annotation.NonNull;

import com.example.voiceacademyapp.api.responds.GenericResponse;
import com.example.voiceacademyapp.model.PodcastChannel;
import com.example.voiceacademyapp.service.client.PodcastClient;

import retrofit2.Call;
import retrofit2.Callback;

public class PodcastService {
    private Context context;
    private static String token;
    private int idUser;
    private String role;
    public PodcastService(Context context){
        this.context = context;
        SharedPreferences sharedPreferences = context.getSharedPreferences("SharedPreferences", Context.MODE_PRIVATE);
        token = sharedPreferences.getString("token", "");
        idUser = sharedPreferences.getInt("id", 0);
        role = sharedPreferences.getString("role", "");
    }

    public static void GetPodcasts(PodcastServiceCallback callback){
        Call<GenericResponse<PodcastChannel>> call = PodcastClient.getInstance().getApiService().getPodcasts("Bearer " + token);
        call.enqueue(new Callback<GenericResponse<PodcastChannel>>() {
            @Override
            public void onResponse(@NonNull Call<GenericResponse<PodcastChannel>> call, @NonNull retrofit2.Response<GenericResponse<PodcastChannel>> response) {
                if (response.isSuccessful()) {
                    if (response.isSuccessful()) {
                        callback.onSuccess(response.body());
                    } else {
                        callback.onSuccess(new GenericResponse<PodcastChannel>(response.code()));
                    }
                } else {
                    callback.onSuccess(new GenericResponse<PodcastChannel>(response.code()));
                }
            }

            @Override
            public void onFailure(@NonNull Call<GenericResponse<PodcastChannel>> call, @NonNull Throwable throwable) {
                callback.onFailure(throwable);
            }
        });
    }

    public interface PodcastServiceCallback {

        void onSuccess(GenericResponse<PodcastChannel> response);

        void onFailure(Throwable throwable);

    }
}
