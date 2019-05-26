package com.AR.test;

import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageButton;

/**
 * Created by Junyoung on 2016-06-23.
 */

public class TabFragment1 extends Fragment {

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.tab_fragment_1, container, false);



        ImageButton imageButton = (ImageButton)v.findViewById(R.id.cameraButton);
        imageButton.setOnClickListener(new View.OnClickListener(){
            public void onClick(View view){
                Intent intent = new Intent(v.getContext(), UnityPlayerActivity.class);
                startActivity(intent);
            }
        });


        return v;
    }
}

