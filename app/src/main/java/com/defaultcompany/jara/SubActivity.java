package com.defaultcompany.jara;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import com.bumptech.glide.Glide;
import com.kakao.usermgmt.UserManagement;
import com.kakao.usermgmt.callback.LogoutResponseCallback;

public class SubActivity extends AppCompatActivity {
    private String strNick, strProfileImg, strGender, strAge;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sub);

        Intent intent = getIntent();
        strNick = intent.getStringExtra("name");
        strProfileImg = intent.getStringExtra("image");
        strGender = intent.getStringExtra("gender");
        strAge = intent.getStringExtra("age");

        TextView tv_nick = findViewById(R.id.tv_nickName);
        TextView tv_gender = findViewById(R.id.tv_gender);
        TextView tv_age = findViewById(R.id.tv_age);
        ImageView iv_profile = findViewById(R.id.iv_profile);

        //회원정보 데이터 set
        tv_nick.setText(strNick);
        tv_gender.setText(strGender);
        tv_age.setText(strAge);

        //프로필 이미지 사진 set
        Glide.with(this).load(strProfileImg).into(iv_profile);

        //로그아웃
        findViewById(R.id.btn_logout).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                UserManagement.getInstance().requestLogout(new LogoutResponseCallback() {
                    @Override
                    public void onCompleteLogout() {
                        //로그아웃 성공시 수행하는 지점
                        finish();//현재 액티비티 종료
                    }
                });
            }
        });


    }
}