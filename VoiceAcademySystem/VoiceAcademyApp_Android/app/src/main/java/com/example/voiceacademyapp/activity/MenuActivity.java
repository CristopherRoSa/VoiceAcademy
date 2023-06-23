package com.example.voiceacademyapp.activity;

import androidx.appcompat.app.AppCompatActivity;
import androidx.databinding.DataBindingUtil;
import androidx.lifecycle.ViewModelProvider;

import android.os.Bundle;

import com.example.voiceacademyapp.DTOs.TokenDTO;
import com.example.voiceacademyapp.R;
import com.example.voiceacademyapp.databinding.ActivityMenuBinding;
import com.example.voiceacademyapp.viewModel.MenuViewModel;

public class MenuActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        ActivityMenuBinding binding = DataBindingUtil.setContentView(this, R.layout.activity_menu);
        MenuViewModel viewModel = new ViewModelProvider(this).get(MenuViewModel.class);
        TokenDTO user = (TokenDTO) getIntent().getSerializableExtra("user");
        viewModel.setUser(user);
        binding.setLifecycleOwner(this);
        binding.setMenuViewModel(viewModel);
    }
}