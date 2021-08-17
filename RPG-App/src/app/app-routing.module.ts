import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CharacterComponent } from './character/character.component';
import { CharacterEditComponent } from './character/characterEdit/characterEdit.component';

const routes: Routes = [
  {path: 'characters', component: CharacterComponent},
  {path: 'character/:id/edit',component: CharacterEditComponent},
  {path: 'character',component: CharacterEditComponent},
  {path: '', redirectTo: 'characters', pathMatch: 'full'},
  {path: '**', redirectTo: 'characters', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
