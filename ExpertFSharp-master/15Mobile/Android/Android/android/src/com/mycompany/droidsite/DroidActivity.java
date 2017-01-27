package com.mycompany.droidsite;

import android.os.Bundle;
import com.intellifactory.android.AsyncActivity;
import com.intellifactory.android.WebSharperView;

public class DroidActivity extends AsyncActivity {
	
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);       
        this.setContentView(new WebSharperView().run(this));
    }    
}
