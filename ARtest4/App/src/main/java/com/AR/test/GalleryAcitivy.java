package com.AR.test;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.ImageButton;

public class GalleryAcitivy extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_gallery_acitivy);

        ImageButton stampButton1 = (ImageButton)findViewById(R.id.StampButton1); // 장미동
        stampButton1.setOnClickListener(new View.OnClickListener(){
            public void onClick(View view){
                Intent StampIntent = new Intent(GalleryAcitivy.this, StampActivity.class);
                StampIntent.putExtra("key", 0);
                startActivity(StampIntent);
            }
        });

        ImageButton stampButton2 = (ImageButton)findViewById(R.id.StampButton2); // 전북청년
        stampButton2.setOnClickListener(new View.OnClickListener(){
            public void onClick(View view){
                Intent StampIntent = new Intent(GalleryAcitivy.this, StampActivity.class);
                StampIntent.putExtra("key", 1);
                startActivity(StampIntent);
            }
        });
    }
}
