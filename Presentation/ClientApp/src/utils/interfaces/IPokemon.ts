import {IGender} from "./IGender";
import {ICategory} from "./ICategory";

export interface IPokemon {
    id: string;
    name: string;
    portrait: string;
    height: number;
    weight: number;
    gender: IGender;
    description: string;
    generation: string;
    specie: string;
    categories : Array<ICategory>;
    statistics: {
        hp: number;
        attack: number;
        defense: number;
        specialAttack: number;
        specialDefense: number;
        speed: number;
        total: number;
    };
}
