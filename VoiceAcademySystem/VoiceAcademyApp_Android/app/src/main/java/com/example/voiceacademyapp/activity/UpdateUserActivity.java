package com.example.voiceacademyapp.activity;

import androidx.appcompat.app.AppCompatActivity;
import androidx.databinding.DataBindingUtil;
import androidx.lifecycle.ViewModelProvider;

import android.os.Bundle;

import com.example.voiceacademyapp.R;
import com.example.voiceacademyapp.databinding.ActivityRegisterUserBinding;
import com.example.voiceacademyapp.databinding.ActivityUpdateUserBinding;
import com.example.voiceacademyapp.viewModel.RegisterUserViewModel;
import com.example.voiceacademyapp.viewModel.UpdateUserViewModel;

public class UpdateUserActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        ActivityUpdateUserBinding binding = DataBindingUtil.setContentView(this, R.layout.activity_update_user);
        UpdateUserViewModel viewModel = new ViewModelProvider(this).get(UpdateUserViewModel.class);
        binding.setLifecycleOwner(this);
        binding.setUpdateUserViewModel(viewModel);
    }
}