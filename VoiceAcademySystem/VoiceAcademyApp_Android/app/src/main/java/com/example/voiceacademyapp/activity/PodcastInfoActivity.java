package com.example.voiceacademyapp.activity;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.example.voiceacademyapp.R;

public class PodcastInfoActivity extends AppCompatActivity {

    private Context context;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        this.context = PodcastInfoActivity.this;
        setContentView(R.layout.activity_podcast_info);
        int idPodcast = getIntent().getIntExtra("idPodcast", 0);
        TextView textIdPodcast = findViewById(R.id.textIdPodcast);
        textIdPodcast.setText(String.valueOf(idPodcast));
        Button btnMenu2 = findViewById(R.id.btnMenu2);
        btnMenu2.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                // Aquí puedes colocar el código que deseas ejecutar cuando se haga clic en el botón
                // Por ejemplo:
                Intent intent = new Intent(context, PodcastsActivity.class);
                intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TASK);
                context.startActivity(intent);
                Toast.makeText(getApplicationContext(), "Botón de menú clickeado", Toast.LENGTH_SHORT).show();
            }
        });
    }
}