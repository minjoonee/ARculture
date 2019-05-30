package com.AR.test;

import android.content.Context;
import android.os.Environment;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.MotionEvent;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.ScrollView;
import android.widget.TextView;
import android.app.AlertDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.TabLayout;
import android.support.v4.view.ViewPager;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.View;
import android.widget.ImageButton;
import java.io.File;
import java.io.FileReader;
import java.io.*;
import java.util.ArrayList;
/*
    전북 도립 미술관
    - 장미동 미술관 jb01 ~ jb36
    - 전북청년 미술관 jb37, 39, 40
 */

class STAMP // 스탬프 클래스 배열로 만들어서 스탬프 기능 동작할수 있게끔
{
    public String StampName; // 파일입출력에서 가져온 파일명
    public int StampCheck; // 트래킹 확인 변수
    public String StampAuthor; // 작가명
    public String StampWork; // 작품명

    public STAMP(String SName, int SCheck){
        this.StampName = SName;
        this.StampCheck = SCheck;
    }
    public STAMP(String SName, int SCheck, String SAuthor, String SWork)
    {
        this.StampName = SName;
        this.StampCheck = SCheck;
        this.StampAuthor = SAuthor;
        this.StampWork = SWork;
    }
}

public class StampActivity extends AppCompatActivity {
    /* 이런식으로 나눌 수 있지만 현재 전북도립 미술관은 동일한 파일명으로 구분된 상태 ex) jb01~ jb40
    STAMP[] jb_stamp_jangmi = new STAMP[34];//장미동은 34개
    STAMP[] jb_stamp_jbnu = new STAMP[3]; // 전북청년 35~40 5개
    */
    STAMP[] jb_stamp = new STAMP[37];// 1~10, 12~30, 32~37, 39~40 총 37개 11, 31, 38 제외


    public String jb_Author[];
    public String jb_Work[];

    ArrayList<String> Stamp_List = new ArrayList<String>();
    private ListView stampListView;
    private ScrollView scrollView;

    int Num = 37; // jb미술관 개수 37개, Num변수는 미술관 개수 저장해두는 변수.
    int count; // 미술관에서 인식한 갯수 세는 변수
    int key; // 미술관 프래그먼트에서 보내준 키값
    int start; // 전북 도립 미술관에 한정된 변수. 시작값을 통해 인식개수 확인
    int finish; //마찬가지
    String keyword;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_stamp);

        jb_Author = getAuthor();
        jb_Work = getWork();

        String museumName="";
        String line = null;
        String str = getExternalFilesDir(null).getPath();

        File saveFile = new File(str+ "/ARculture"); // 저장 경로
        try {
            BufferedReader buf = new BufferedReader(new FileReader(saveFile+"/Stamp.txt"));
            while((line=buf.readLine())!=null){
                Stamp_List.add(line); // 한줄씩 읽어서 리스트에 저장
            }
            buf.close();
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }


        Intent intent = getIntent();
        key = intent.getExtras().getInt("key");
        switch (key){ // 키로 받은 미술관 값에 미술관 갯수 부여
            case 0:
                Num = 37;
                museumName = getString(R.string.g1_name);
                keyword = "jb";
                break;
            case 1:
                Num = 37;
                museumName = getString(R.string.g2_name);
                keyword = "jb";
                break;
            default:
                museumName = "DEFAULT";
                Num = -1;
        }

        StampInput(); // 입력받는 스탬프 배열 초기화
        StampFile(); // 트래킹해서 저장된 리스트 입력+스탬프 카운트

        TextView stampView = (TextView)findViewById(R.id.StampText);
        stampView.setText(museumName + getString(R.string.detect_num));
        int temp= finish - start;
        stampView.append(count+" / "+ temp+"\n");
        /*
        for (int i=start; i<finish; i++){
            if(jb_stamp[i].StampCheck == 1){
                stampView.append("인식 완료 - 작가 : "+jb_stamp[i].StampAuthor+", 작품명 : "+jb_stamp[i].StampWork+"\n");
            }
        }
        for (int i=start; i<finish; i++){
            if(jb_stamp[i].StampCheck == 0){
                stampView.append("인식 안됨 - 작가 : "+jb_stamp[i].StampAuthor+", 작품명 : "+jb_stamp[i].StampWork+"\n");
            }
        }
        */
        stampListView = (ListView)findViewById(R.id.StampListView);
        scrollView = (ScrollView)findViewById(R.id.scrollView);
        stampListView.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                scrollView.requestDisallowInterceptTouchEvent(true);
                return false;
            }
        });
        ListSetting();

    }


    public void StampInput(){//1~10, 12~30, 32~37, 39~40 총 37개
        int i, chk = 1;
        for(i=0; i<Num; i++,chk++){
            if(chk==11 || chk == 31|| chk==38) ++chk; //이건 전적으로 중간 비어있는 사진 때문에 넣은 조건문. 다음에 사진 추가할때는 연속적인 숫자로 없어야될 구문
            if(chk >= 10){ // 파일명도 01이 아닌 그냥 1로 시작해야 함을 의미
                Log.e("STAMP",jb_Work.length+" "+jb_Author.length+" "+jb_Author[i]+" "+jb_Work[i]);
                jb_stamp[i] = new STAMP(keyword+String.valueOf(chk), 0, jb_Author[i], jb_Work[i]); // 파일 이름 jb01~
            }
            else{
                jb_stamp[i] = new STAMP(keyword+"0"+ String.valueOf(chk), 0, jb_Author[i], jb_Work[i]); // 파일 이름 jb01~
            }
        }
    }


    public void StampFile(){
        int i, j;
        switch(key){ // 전북도립 미술관의 경우 하나로 통합된 파일의 배열 시작 부분을 정해줘야됨.
            case 0:
                start = 0;
                finish = 30;
                break;
            case 1:
                start = 30;
                finish = Num;
                break;
        }

        for(i=0; i<Stamp_List.size(); i++){
            for(j=start; j<finish; j++){
                if(jb_stamp[j].StampName.equals(Stamp_List.get(i))){
                    jb_stamp[j].StampCheck = 1;
                    count++;
                    break;
                }
            }
        }
    }
    private void ListSetting(){

        MyAdapter mMyAdapter = new MyAdapter();

        for(int i=start; i<finish; i++){
            if(jb_stamp[i].StampCheck == 1){
                mMyAdapter.addItem(ContextCompat.getDrawable(getApplicationContext(),R.drawable.clear), jb_stamp[i].StampAuthor, jb_stamp[i].StampWork);
            }
            else{
                mMyAdapter.addItem(ContextCompat.getDrawable(getApplicationContext(),R.drawable.no_clear), jb_stamp[i].StampAuthor, jb_stamp[i].StampWork);
            }
        }

        /* 리스트뷰에 어댑터 등록 */
        stampListView.setAdapter(mMyAdapter);
    }
    public String[] getAuthor(){
        String i_author0 = getString(R.string.kimyoungkyung);
        String i_author1 = getString(R.string.kusatbyul);
        String i_author2 = getString(R.string.kunsan);
        String i_author3 = getString(R.string.sinsukho);
        String i_author4 = getString(R.string.parkduri);
        String i_author5 = getString(R.string.kimyoungbong);

        String jb_Author[] = new String[]{
                i_author0, i_author0,i_author0,i_author0,i_author0,i_author0,i_author0,i_author2,i_author2,i_author2
                ,i_author3,i_author1,i_author1,i_author1,i_author1,i_author1,i_author1,i_author0,i_author0,i_author0
                ,i_author0,i_author0,i_author0,i_author0,i_author2,i_author2,i_author2,i_author2,i_author2,i_author1
                ,i_author4,i_author4, i_author4,i_author4,i_author4,i_author5,i_author5
        };

        return jb_Author;
    }

    public String[] getWork(){


        String jb_Work[] = new String[]{
                getString(R.string.jb1),getString(R.string.jb2),getString(R.string.jb3),getString(R.string.jb4),getString(R.string.jb5),getString(R.string.jb6), getString(R.string.jb7),getString(R.string.jb8),
                getString(R.string.jb9),getString(R.string.jb10),getString(R.string.jb12),getString(R.string.jb13),getString(R.string.jb14),getString(R.string.jb15),getString(R.string.jb16),getString(R.string.jb17),
                getString(R.string.jb18),getString(R.string.jb19),getString(R.string.jb20),getString(R.string.jb21),getString(R.string.jb22),getString(R.string.jb23),getString(R.string.jb24),getString(R.string.jb25),
                getString(R.string.jb26),getString(R.string.jb27),getString(R.string.jb28),getString(R.string.jb29),getString(R.string.jb30),getString(R.string.jb32),getString(R.string.jb33),getString(R.string.jb33),
                getString(R.string.jb34),getString(R.string.jb35),getString(R.string.jb36),getString(R.string.jb37),getString(R.string.jb39),getString(R.string.jb40)
        };

        return jb_Work;
    }
}
