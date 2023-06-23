package com.example.voiceacademyapp.viewModel;

import android.app.Application;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.widget.ImageView;

import androidx.annotation.NonNull;
import androidx.lifecycle.AndroidViewModel;
import androidx.lifecycle.MutableLiveData;

import com.example.voiceacademyapp.activity.MenuActivity;
import com.example.voiceacademyapp.activity.UpdateUserActivity;
import com.example.voiceacademyapp.service.services.LoginService;
import androidx.databinding.BindingAdapter;


public class AccountViewModel extends AndroidViewModel {

    private final MutableLiveData<String> name = new MutableLiveData<>();
    private final MutableLiveData<String> lastName = new MutableLiveData<>();
    private final MutableLiveData<Integer> age = new MutableLiveData<>();
    private final MutableLiveData<String> email = new MutableLiveData<>();
    private final MutableLiveData<Bitmap> imageUser = new MutableLiveData<>();
    private final Context context;
    public AccountViewModel(@NonNull Application application) {
        super(application);
        context = application.getApplicationContext();

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

    public void setAge(Integer profileAge) {
        age.setValue(profileAge);
    }

    public MutableLiveData<String> getEmail() {
        return email;
    }

    public void setEmail(String profileEmail) {
        email.setValue(profileEmail);
    }

    public MutableLiveData<Bitmap> getImageUser() {
        return imageUser;
    }
    @BindingAdapter("imageBitmap")
    public static void setImageUser(ImageView imageView, Bitmap bitmap) {
        imageView.setImageBitmap(bitmap);
    }
    public void goToMainMenu() {
        Intent intent = new Intent(context, MenuActivity.class);
        intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TASK);
        context.startActivity(intent);
    }

    public void goToUpdateUser() {
        Intent intent = new Intent(context, UpdateUserActivity.class);
        intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TASK);
        context.startActivity(intent);
    }
}
