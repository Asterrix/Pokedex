export interface IGender {
    male:boolean;
    female:boolean;
}

export interface ICategory {
    category: {
        id: string;
        categoryName: string;    
    }
}

export interface IPokemon {
    id: string;
    name: string;
    portrait: string;
    height: number;
    weight: number;
    gender: IGender;
    description: string;
    generation: string;
    species: string;
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