package com.AR.test;

import android.content.Context;
import android.os.Bundle;
import android.os.Environment;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import org.w3c.dom.Text;

import java.io.File;

/**
 * Created by Junyoung on 2016-06-23.
 */

public class TabFragment3 extends Fragment {

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {

        View v = inflater.inflate(R.layout.tab_fragment_3, container, false);
        TextView test = (TextView)v.findViewById(R.id.test);
        test.setText(Environment.getDataDirectory()+ "/ARculture");

        return v;
    }
}
