import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule} from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MaterialModule } from './app.material.module';

import { ToastrModule } from 'ngx-toastr';

import { ToolbarComponent } from './toolbar/toolbar.component';
import { TitleComponent } from './_shared/title/title.component'
import { CharacterComponent, CharacterDialogComponent } from './character/character.component';
import { CharacterEditComponent } from './character/characterEdit/characterEdit.component';


@NgModule({
  declarations: [	
    AppComponent,
    ToolbarComponent,
    TitleComponent,
    CharacterComponent, CharacterDialogComponent,
    CharacterEditComponent
   ],
  imports: [
    HttpClientModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot({
      timeOut: 10000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }),
    FlexLayoutModule,
    MaterialModule
  ],
  exports: [ ],
  providers: [ ],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ],
  bootstrap: [AppComponent]
})
export class AppModule { }
