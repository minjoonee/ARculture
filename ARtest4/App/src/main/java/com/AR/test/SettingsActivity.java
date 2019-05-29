package com.AR.test;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.res.Configuration;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.RelativeLayout;
import android.widget.TextView;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileWriter;
import java.io.IOException;
import java.util.Locale;

import butterknife.BindView;
import butterknife.OnClick;

public class SettingsActivity extends AppCompatActivity {


    // 설정 창에서 각 버튼 클릭하면
    // 앱 내에서 디바이스 언어를 변경 하도록 해준다.

    int LangNum;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_settings);
        LangNum = -1;
    }

    public void LangWriteFunc(){
        String line = null;
        String str = getExternalFilesDir(null).getPath();
        File saveFile = new File(str+ "/ARculture"); // 저장 경로
        if(!saveFile.exists()){ // 폴더 없을 경우
            saveFile.mkdir(); // 폴더 생성
        }

        try {
            BufferedWriter buf = new BufferedWriter(new FileWriter(saveFile+"/Language.txt", false));
            buf.append(String.valueOf(LangNum)); //쓰기
            buf.close();
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
    public void setting_Korean(View view){
        Locale lang = Locale.KOREA;
        Configuration config = new Configuration();
        config.locale = lang;
        getResources().updateConfiguration(config, getResources().getDisplayMetrics());
        //Intent intent = new Intent(SettingsActivity.this, MainActivity.class);
        //intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP); // 액티비티 재사용이라서 언어적용이 안됨

        LangNum = 1;
        LangWriteFunc();
        MainActivity activity = (MainActivity)MainActivity._MainActivity;
        activity.finish();
        startActivity(new Intent(SettingsActivity.this, MainActivity.class));
        this.finish();
    }

    public void setting_English(View view){
        Locale lang = Locale.ENGLISH;
        Configuration config = new Configuration();
        config.locale = lang;
        getResources().updateConfiguration(config, getResources().getDisplayMetrics());

        LangNum = 3;
        LangWriteFunc();
        MainActivity activity = (MainActivity)MainActivity._MainActivity;
        activity.finish();
        startActivity(new Intent(SettingsActivity.this, MainActivity.class));
        this.finish();
    }

    public void setting_Japan(View view){
        Locale lang = Locale.JAPAN;
        Configuration config = new Configuration();
        config.locale = lang;
        getResources().updateConfiguration(config, getResources().getDisplayMetrics());

        LangNum = 4;
        LangWriteFunc();
        MainActivity activity = (MainActivity)MainActivity._MainActivity;
        activity.finish();
        startActivity(new Intent(SettingsActivity.this, MainActivity.class));
        this.finish();
    }

    public void setting_China(View view){
        Locale lang = Locale.CHINESE;
        Configuration config = new Configuration();
        config.locale = lang;
        getResources().updateConfiguration(config, getResources().getDisplayMetrics());

        LangNum = 2;
        LangWriteFunc();
        MainActivity activity = (MainActivity)MainActivity._MainActivity;
        activity.finish();
        startActivity(new Intent(SettingsActivity.this, MainActivity.class));
        this.finish();
    }
}
