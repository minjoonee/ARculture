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
    public String jb_Author[] = new String[]{"김영경", "김영경","김영경","김영경","김영경","김영경","김영경","군산근대역사박물관","군산근대역사박물관","군산근대역사박물관"
                                        ,"신석호","구샛별","구샛별","구샛별","구샛별","박두리","박두리","김영경","김영경"
                                        ,"김영경","김영경","김영경","김영경","김영경","군산근대역사박물관","군산근대역사박물관","군산근대역사박물관","군산근대역사박물관","군산근대역사박물관"
                                        ,"구샛별","구샛별","구샛별","박두리","박두리","박두리","김영봉","김영봉"};
    public String jb_Work[] = new String[]{"송창동 타일#1", "송창동 타일#2","송창동 타일 #3","안녕, 신흥동 타일","안녕, 신흥동_꽃무늬 장판","안녕, 신흥동_무너진 집의 구조물","안녕, 신흥동_타일 #05","거류민단 시절의 도로", "시가지 전경","시가지 전경"
                                        ,"군산 오식도","구석","부서진 벽","붉은 집","세개의 방","'한줄한줄' '쿵' '데구르르' 1","먼지연못","안녕, 신흥동","오래된 망각_서래로43-1"
                                        ,"오래된 망각_처녀점술원","오래된 망각_까치만화","퇴적된 도시_경암동 #3","퇴적된 도시_경암동 #1","퇴적된 도시_경암동 #2","개항 이전의 군산","창고 밖 미곡","수탈미곡 선적광경","창고 안 미곡","군산항"
                                        ,"붉은 타일","네모난 방, 세모난 방","둥근 벽","선데이","과도한 친절","4컷 드로잉","잿빛 드로잉","잿빛 드로잉"};
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
                museumName = "장미동 미술관";
                keyword = "jb";
                break;
            case 1:
                Num = 37;
                museumName = "전북청년 미술관";
                keyword = "jb";
                break;
            default:
                museumName = "알수없음";
                Num = -1;
        }

        StampInput(); // 입력받는 스탬프 배열 초기화
        StampFile(); // 트래킹해서 저장된 리스트 입력+스탬프 카운트

        TextView stampView = (TextView)findViewById(R.id.StampText);
        stampView.setText(museumName + " 인식 완료 개수 : 총 개수\n");
        int temp= finish - start;
        stampView.append(count+" : "+ temp+"\n");
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
                finish = 32;
                break;
            case 1:
                start = 32;
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
}
