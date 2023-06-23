package com.example.voiceacademyapp.activity;

import androidx.appcompat.app.AppCompatActivity;
import androidx.databinding.DataBindingUtil;
import androidx.lifecycle.ViewModelProvider;

import android.os.Bundle;

import com.example.voiceacademyapp.R;
import com.example.voiceacademyapp.databinding.ActivityLoginBinding;
import com.example.voiceacademyapp.databinding.ActivityRegisterUserBinding;
import com.example.voiceacademyapp.viewModel.LoginViewModel;
import com.example.voiceacademyapp.viewModel.RegisterUserViewModel;

public class RegisterUserActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        ActivityRegisterUserBinding binding = DataBindingUtil.setContentView(this, R.layout.activity_register_user);
        RegisterUserViewModel viewModel = new ViewModelProvider(this).get(RegisterUserViewModel.class);
        binding.setLifecycleOwner(this);
        binding.setRegisterUserViewModel(viewModel);
    }
}