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

public class TabFragment2 extends Fragment {
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.tab_fragment_2, container, false);

        ImageButton stampButton1 = (ImageButton)v.findViewById(R.id.StampButton1); // 장미동
        stampButton1.setOnClickListener(new View.OnClickListener(){
            public void onClick(View view){
                Intent StampIntent = new Intent(getActivity().getApplicationContext(), StampActivity.class);
                StampIntent.putExtra("key", 0);
                startActivity(StampIntent);
            }
        });

        ImageButton stampButton2 = (ImageButton)v.findViewById(R.id.StampButton2); // 전북청년
        stampButton2.setOnClickListener(new View.OnClickListener(){
            public void onClick(View view){
                Intent StampIntent = new Intent(getActivity().getApplicationContext(), StampActivity.class);
                StampIntent.putExtra("key", 1);
                startActivity(StampIntent);
            }
        });

        return v;
    }

}
