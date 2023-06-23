package com.example.voiceacademyapp.DTOs;

import androidx.databinding.InverseMethod;

public class IntegerConverter {
    @InverseMethod("convertToString")
    public static Integer convertToInteger(String value) {
        try {
            return Integer.parseInt(value);
        } catch (NumberFormatException e) {
            return 0; // Valor predeterminado en caso de error de conversión
        }
    }

    public static String convertToString(Integer value) {
        return value != null ? String.valueOf(value) : "";
    }
}
