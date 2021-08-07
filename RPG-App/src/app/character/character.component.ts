import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';

import { environment } from 'src/environments/environment';

import { Character } from '../_models/Character';
import { CharacterService } from '../_services/character.service';

import { ToastrService } from 'ngx-toastr';

export interface DialogData {
  character: Character;
}

@Component({
  selector: 'app-character',
  templateUrl: './character.component.html',
  styleUrls: ['./character.component.css']
})

export class CharacterComponent implements OnInit, AfterViewInit {

  characters: Character[];

  length = 0;
  pageSize = 4;
  pageSizeOptions: number[] = [3, 4, 5];
  pageEvent: PageEvent;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    private characterService: CharacterService,
    private dialog: MatDialog,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.getCharacters();
  }

  ngAfterViewInit() {
    this.paginator.page.subscribe(() => {
        this.pageSize = this.paginator.pageSize;
        this.characterService.getCharacters(this.paginator.pageIndex + 1, this.paginator.pageSize).subscribe((response: any) => {
          this.characters = response.characterDtos;
          this.length = response.pagination.totalCount;
        }, error => { console.log(error); });
    });
  }

  getCharacters() {
    this.characterService.getCharacters(1, this.pageSize).subscribe((response: any) => {
      this.characters = response.characterDtos;
      this.length = response.pagination.totalCount;
    }, error => { console.log(error); });
  }

  getImage(imageURL: string) {
    return imageURL !== '' ? `${environment.apiURL}Images/${imageURL}` : `${environment.apiURL}Images/character_and_morty.png`;
  }

  openDialog(id: number) {
    const dialogRef = this.dialog.open(CharacterDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.characterService.deleteCharacter(id).subscribe(
          () => {
              this.getCharacters();
              this.toastr.success('Excluído com sucesso!', 'Excluir');
            }, error => {
              this.toastr.error('Erro ao Excluir', 'Excluir');
            }
        );
      }
    });
  }
}

@Component({
  selector: 'app-character.dialog.component',
  templateUrl: 'character.dialog.component.html',
})
export class CharacterDialogComponent {}
