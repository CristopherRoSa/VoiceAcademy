package com.example.voiceacademyapp.viewModel;

import android.app.Application;
import android.content.Context;
import android.content.Intent;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.lifecycle.AndroidViewModel;
import androidx.lifecycle.MutableLiveData;

import com.example.voiceacademyapp.DTOs.EditUserDTO;
import com.example.voiceacademyapp.DTOs.LoginDTO;
import com.example.voiceacademyapp.DTOs.UserDTO;
import com.example.voiceacademyapp.R;
import com.example.voiceacademyapp.activity.AccountActivity;
import com.example.voiceacademyapp.activity.MenuActivity;
import com.example.voiceacademyapp.api.responds.GenericResponse;
import com.example.voiceacademyapp.model.User;
import com.example.voiceacademyapp.service.services.UserService;

import java.net.HttpURLConnection;

public class UpdateUserViewModel extends AndroidViewModel {
    private final MutableLiveData<String> name = new MutableLiveData<>();
    private final MutableLiveData<String> lastName = new MutableLiveData<>();
    private final MutableLiveData<Integer> age = new MutableLiveData<>();

    private Context context;
    private UserService userService;
    public UpdateUserViewModel(@NonNull Application application) {
        super(application);
        context = application.getApplicationContext();
        userService = new UserService(context);
    }

    public MutableLiveData<String> getName() {
        return name;
    }

    public void setName(String profileName) {
        name.setValue(profileName);
    }

    public MutableLiveData<String> getLastName() {
        return lastName;
    }

    public void setLastName(String profileLastName) {
        lastName.setValue(profileLastName);
    }

    public MutableLiveData<Integer> getAge() {
        return age;
    }

    public void setAge(Integer updateAge) {
        age.setValue(updateAge);
    }

    public void goToAccount() {
        Intent intent = new Intent(context, AccountActivity.class);
        intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TASK);
        context.startActivity(intent);
    }
    public void onSaveButtonClicked() {
        if (name.getValue() != null & lastName.getValue() != null & age.getValue() != null){
            EditUserDTO user = new EditUserDTO();
            user.setName(name.getValue());
            user.setLastName(lastName.getValue());
            user.setAge(age.getValue());
            updateUser(user);
        } else {
            Toast.makeText(context, R.string.empty_fields_label, Toast.LENGTH_SHORT).show();
        }
    }

    private void updateUser(EditUserDTO user) {
        UserService.updateUser(user, new UserService.UserServiceCallback() {
            @Override
            public void onSuccess(GenericResponse<User> response) {
                int code = response.getCode();
                if (code == HttpURLConnection.HTTP_FORBIDDEN) {
                    Toast.makeText(context, R.string.expired_session_label, Toast.LENGTH_SHORT).show();
                } else if (code == HttpURLConnection.HTTP_NOT_FOUND || code == HttpURLConnection.HTTP_BAD_REQUEST){
                    Toast.makeText(context, R.string.invalid_data_label, Toast.LENGTH_SHORT).show();
                } else {
                    Toast.makeText(context, R.string.update_user_success_label, Toast.LENGTH_SHORT).show();
                    goToAccount();
                }
            }

            @Override
            public void onFailure(Throwable throwable) {
                Toast.makeText(context, R.string.service_not_available_label, Toast.LENGTH_SHORT).show();
            }
        });
    }
}
