import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Character } from '../_models/Character';

@Injectable({
  providedIn: 'root'
  })
  export class CharacterService {

  constructor(private http: HttpClient) { }

  baseURL = environment.apiURL + 'api/Character';

  getCharacters(pageNumber: number, pageSize: number): Observable<Character[]> {
    return this.http.get<Character[]>(`${this.baseURL}?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  getCharactersById(id: number): Observable<Character> {
    return this.http.get<Character>(`${this.baseURL}/${id}`);
  }

  postCharacter(Character: Character) {
    return this.http.post(this.baseURL, Character);
  }

  postUpload(CharacterId: number, file: File) {
    const fileToUpload = file as File;
    const formData = new FormData();
    formData.append('file', fileToUpload);
    return this.http.post(`${this.baseURL}/upload-image/${CharacterId}`, formData);
  }

  putCharacter(Character: Character) {
    return this.http.put(`${this.baseURL}/${Character.id}`, Character);
  }

  deleteCharacter(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }
}
