package com.example.voiceacademyapp.DTOs;

public class UserDTO {
    private int idUser;
    private String email;
    private int age;
    private String name;
    private String lastName;
    private String password;
    private byte[] image;

    public UserDTO(int idUser, String email, int age, String name, String lastName, String password, byte[] image) {
        this.idUser = idUser;
        this.email = email;
        this.age = age;
        this.name = name;
        this.lastName = lastName;
        this.password = password;
        this.image = image;
    }

    public UserDTO() {

    }

    public int getIdUser() {
        return idUser;
    }

    public void setIdUser(int idUser) {
        this.idUser = idUser;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public int getAge() {
        return age;
    }

    public void setAge(int age) {
        this.age = age;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public byte[] getImage() {
        return image;
    }

    public void setImage(byte[] image) {
        this.image = image;
    }
}
