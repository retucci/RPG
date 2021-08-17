import { Move } from './Move';
import { Weapon } from './Weapon';

export class Character {
    id : number;
    name: string;
    nickName: string;
    gender: string;
    mainType: number;
    secondaryType: number;
    image: string;
    level: number;
    totalExperiencePoints: number;
    experiencePoints: number;
    totalHitPoints: number;
    hitPoints: number;
    attack: number;
    defense: number;
    specialAttack: number;
    specialDefense: number;
    speed: number;
    weapons: Weapon[];
    moves: Move[];
}
