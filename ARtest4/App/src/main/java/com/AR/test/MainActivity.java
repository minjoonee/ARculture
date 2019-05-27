package com.AR.test;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.design.widget.TabLayout;
import android.support.v4.view.ViewPager;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.TypedValue;
import android.view.View;
import android.widget.ImageButton;
import android.widget.TextView;

import java.util.Locale;

public class MainActivity extends AppCompatActivity {

    public SharedPreferences prefs;
    public TextView logoText;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        prefs = getSharedPreferences("Pref", MODE_PRIVATE);

        logoText = findViewById(R.id.App_logo);
        getLanguage();
        checkFirstRun();
    }

    public void getLanguage(){
        Locale locale = getResources().getConfiguration().locale;
        String language = locale.getLanguage();
        switch(language){
            case "en":
                logoText.setTextSize(TypedValue.COMPLEX_UNIT_DIP,50);
                break;
            case "ja":
                logoText.setTextSize(TypedValue.COMPLEX_UNIT_DIP,50);
                break;
            case "ko":
                logoText.setTextSize(TypedValue.COMPLEX_UNIT_DIP,60);
                break;
            case "zh":
                logoText.setTextSize(TypedValue.COMPLEX_UNIT_DIP,50);
                break;
        }
    }

    public void checkFirstRun(){
        boolean isFirstRun = prefs.getBoolean("isFirstRun",true);
        if(isFirstRun)
        {
            Intent newIntent = new Intent(MainActivity.this, HowToUseActivity.class);
            startActivity(newIntent);

            prefs.edit().putBoolean("isFirstRun",false).apply();
        }
    }

    public void intentARcamera(View view){
        Intent intent = new Intent(MainActivity.this, UnityPlayerActivity.class);
        startActivity(intent);
    }
    public void intentGallery(View view){
        Intent intent = new Intent(MainActivity.this, GalleryAcitivy.class);
        startActivity(intent);
    }
    public void intentSettings(View view){
        Intent intent = new Intent(MainActivity.this, SettingsActivity.class);
        startActivity(intent);
    }
    public void intentHowToUse(View view){
        Intent intent = new Intent(MainActivity.this, HowToUseActivity.class);
        startActivity(intent);
    }
}
