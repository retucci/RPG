import { Move } from './Move';
import { Weapon } from './Weapon';

export class Character {
    id : number;
    name: string;
    image: string;
    level: number;
    experiencePoints: number;
    hitPoints: number;
    attack: number;
    defense: number;
    specialAttack: number;
    specialDefense: number;
    speed: number;
    weapons: Weapon[];
    moves: Move[];
}
