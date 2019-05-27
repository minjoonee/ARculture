package com.AR.test;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.res.Configuration;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.RelativeLayout;
import android.widget.TextView;

import java.util.Locale;

import butterknife.BindView;
import butterknife.OnClick;

public class SettingsActivity extends AppCompatActivity {


    // 설정 창에서 각 버튼 클릭하면
    // 앱 내에서 디바이스 언어를 변경 하도록 해준다.

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_settings);
    }

    public void setting_Korean(View view){
        Locale lang = Locale.KOREA;
        Configuration config = new Configuration();
        config.locale = lang;
        getResources().updateConfiguration(config, getResources().getDisplayMetrics());

        startActivity(new Intent(SettingsActivity.this, MainActivity.class));
        this.finish();
    }

    public void setting_English(View view){
        Locale lang = Locale.ENGLISH;
        Configuration config = new Configuration();
        config.locale = lang;
        getResources().updateConfiguration(config, getResources().getDisplayMetrics());

        startActivity(new Intent(SettingsActivity.this, MainActivity.class));
        this.finish();
    }

    public void setting_Japan(View view){
        Locale lang = Locale.JAPAN;
        Configuration config = new Configuration();
        config.locale = lang;
        getResources().updateConfiguration(config, getResources().getDisplayMetrics());

        startActivity(new Intent(SettingsActivity.this, MainActivity.class));
        this.finish();
    }

    public void setting_China(View view){
        Locale lang = Locale.CHINESE;
        Configuration config = new Configuration();
        config.locale = lang;
        getResources().updateConfiguration(config, getResources().getDisplayMetrics());

        startActivity(new Intent(SettingsActivity.this, MainActivity.class));
        this.finish();
    }
}
