package com.AR.test.fragment;

import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentPagerAdapter;

public class HowToUseAdapter extends FragmentPagerAdapter {
    private static final int HOW_TO_USE_PAGES = 4;

    public HowToUseAdapter(FragmentManager fm){
        super(fm);
    }

    @Override
    public Fragment getItem(int position) {
        return HowToUseFragmentFactory.newInstance(position);
    }

    @Override
    public int getCount() {
        return HOW_TO_USE_PAGES;
    }
}

