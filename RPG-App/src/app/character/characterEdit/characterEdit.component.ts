import { Component, OnInit, Inject } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { environment } from 'src/environments/environment';

import { Character } from 'src/app/_models/Character';
import { CharacterService } from 'src/app/_services/character.service';
import { TypeEnum, TypeEnumLabel } from 'src/app/_enums/TypeEnum.enum';
import { GenderEnum, GenderEnumLabel } from 'src/app/_enums/GenderEnum.enum';
import { CategoryEnum, CategoryEnumLabel } from 'src/app/_enums/CategoryEnum.enum';

@Component({
  selector: 'app-characterEdit',
  templateUrl: './characterEdit.component.html',
  styleUrls: ['./characterEdit.component.css']
})
export class CharacterEditComponent implements OnInit {

  title = "Character"

  registerForm: FormGroup;
  character: Character;

  public typeEnumLabel = TypeEnumLabel;
  public types = Object.values(TypeEnum).filter(value => typeof value === 'number');

  public genderEnumLabel = GenderEnumLabel;
  public genders = Object.values(GenderEnum);

  public categoryEnumLabel = CategoryEnumLabel;
  public categories = Object.values(CategoryEnum);
  
  id = 0;
  code: string;

  imageURL = 'assets/img/upload.png';
  file: File;

  get weapons(): FormArray {
    return this.registerForm.get('weapons') as FormArray;
  }

  get moves(): FormArray {
    return this.registerForm.get('moves') as FormArray;
  }

  constructor(
    public characterService: CharacterService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.validation();
    this.loadCharacter();
  }

  validation() {
    this.registerForm = this.formBuilder.group({
      id: [0],
      name: ['', [Validators.required, Validators.maxLength(100)]],
      nickName: ['', [Validators.required, Validators.maxLength(100)]],
      gender: ['', Validators.required],
      mainType: ['', Validators.required],
      secondaryType: [''],
      image: [''],
      level: ['', [Validators.required, Validators.min(0), Validators.max(999)]],
      experiencePoints: ['', [Validators.required, Validators.min(0), Validators.max(999)]],
      totalExperiencePoints: ['', [Validators.required, Validators.min(0), Validators.max(999)]],
      hitPoints: ['', [Validators.required, Validators.min(0), Validators.max(999)]],
      totalHitPoints: ['', [Validators.required, Validators.min(0), Validators.max(999)]],
      attack: ['', [Validators.required, Validators.min(0), Validators.max(999)]],
      defense: ['', [Validators.required, Validators.min(0), Validators.max(999)]],
      specialAttack: ['', [Validators.required, Validators.min(0), Validators.max(999)]],
      specialDefense: ['', [Validators.required, Validators.min(0), Validators.max(999)]],
      speed: ['', [Validators.required, Validators.min(0), Validators.max(999)]],
      moves: this.formBuilder.array([])
      // weapons: this.formBuilder.array([])
    });
  }

  // addWeapon() {
  //   this.weapons.push(this.createWeapon({ id: 0, code: '', original: 1 }));
  // }

  // createWeapon(weapon: any): FormGroup {
  //   return this.formBuilder.group({
  //       id: [weapon.id],
  //       damage: ['', [Validators.required, Validators.min(0), Validators.max(999)]],
  //       durability: ['', [Validators.required, Validators.min(0), Validators.max(999)]]
  //    });
  // }

  addMove() {
    this.moves.push(this.createMove({ id: 0, name:'',type:'', category:'',accurace:'',damage: '', usage: '' }));
  }

  removeMove(index : number) {
    this.moves.removeAt(index);
  }

  createMove(move: any): FormGroup {
    return this.formBuilder.group({
        id: [move.id],
        name: [move.name, [Validators.required, Validators.maxLength(100)]],
        type: [move.type,[Validators.required]],
        category: [move.category,[Validators.required]],
        accurace: [move.accurace,[Validators.required]],
        damage: [move.damage, [Validators.required, Validators.min(0), Validators.max(999)]],
        usage: [move.usage, [Validators.required, Validators.min(0), Validators.max(999)]]
     });
  }

  loadCharacter() {

    if (!this.route.snapshot.paramMap.has('id')) {
        this.id = 0;
        return;
    }

    if (!Number(this.route.snapshot.paramMap.get('id')) || Number(this.route.snapshot.paramMap.get('id')) <= 0) {
      this.id = 0;
      this.toastr.warning('Não foi possível recuperar o personagem', 'Editar');
      return;
    }

    this.id = +this.route.snapshot.paramMap.get('id');

    if (this.id > 0)  {
        this.characterService.getCharactersById(this.id).subscribe((character: Character) => {
          if (character != null) {
            this.character = Object.assign({}, character);
            this.registerForm.patchValue(character);
            this.character.moves.forEach(move =>{ this.moves.push(this.createMove(move)); })
            if (this.character.image !== '') {
              this.imageURL = environment.apiURL + 'Images/' + this.character.image;
            }
          } else {
              this.toastr.warning('Não foi possível recuperar o personagem', 'Editar');
              this.id = 0;
          }
        }, error => {
           this.toastr.error('Não foi possível recuperar o personagem', 'Editar');
        });
    }
  }

  saveCharacter() {
    if (this.registerForm.valid) {
      if (this.id === 0) {
        this.character = Object.assign({}, this.registerForm.value);
        this.characterService.postCharacter(this.character).subscribe(
          (newCharacter: any) => {
            this.toastr.success('Salvo com sucesso!', 'Salvar');
            this.id = newCharacter.id;
            this.router.navigate([ 'character/', this.id, 'detail']);
          }, error => {
            this.toastr.error('Erro ao Salvar', 'Salvar');
          }
        );
       } else {
        this.character = Object.assign({id: this.character.id}, this.registerForm.value);
        this.characterService.putCharacter(this.character).subscribe(
          () => {
             this.toastr.success('Editado com sucesso!', 'Editar');
          }, error => {
             this.toastr.error('Erro ao Editar', 'Editar');
          }
        );
      }
    }
  }

  onFileChange(ev: any) {
    const reader = new FileReader();
    reader.onload = (event: any) => this.imageURL = event.target.result;
    this.file = ev.target.files[0];
    reader.readAsDataURL(this.file);
    this.uploadImage();
  }

  uploadImage() {
    this.characterService.postUpload(this.id, this.file).subscribe(
      () => {
        this.toastr.success('Imagem atualziada com sucesso!', 'Salvar');
        this.router.navigate([ 'character/', this.id, 'detail']);
      },
      (error: any) => {
        this.toastr.error('Erro ao realizar upload', 'Salvar');
      },
    );
  }
}
