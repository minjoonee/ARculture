package com.AR.test;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.Configuration;
import android.os.Bundle;
import android.os.Debug;
import android.os.PersistableBundle;
import android.support.annotation.Nullable;
import android.support.design.widget.TabLayout;
import android.support.v4.view.ViewPager;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.util.TypedValue;
import android.view.View;
import android.widget.ImageButton;
import android.widget.TextView;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.Locale;

public class MainActivity extends AppCompatActivity {

    public SharedPreferences prefs;
    public TextView logoText;
    public static Activity _MainActivity;
    int LangNum;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        _MainActivity= MainActivity.this;

        prefs = getSharedPreferences("Pref", MODE_PRIVATE);

        logoText = findViewById(R.id.App_logo);
        getLanguage();
        checkFirstRun();
    }


    public void getLanguage(){
        LangNum = -1;
        String line = null;
        String str = getExternalFilesDir(null).getPath();

        File saveFile = new File(str+ "/ARculture"); // 저장 경로
        try {
            BufferedReader buf = new BufferedReader(new FileReader(saveFile+"/Language.txt"));
            while((line=buf.readLine())!=null){
                LangNum = Integer.parseInt(line);
                Log.e("LangNum값"," : "+LangNum);
            }
            buf.close();
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
        Locale lang = getResources().getConfiguration().locale;
        if(LangNum>0){
            switch(LangNum){
                case 1:
                    lang = Locale.KOREA;
                    break;
                case 2:
                    lang = Locale.CHINESE;
                    break;
                case 3:
                    lang = Locale.ENGLISH;
                    break;
                case 4:
                    lang = Locale.JAPAN;
                    break;
            }
            Configuration config = new Configuration();
            config.locale = lang;
        }

        Locale locale = lang;
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
