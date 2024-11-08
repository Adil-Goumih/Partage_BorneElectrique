export class Borne{
    constructor(public borneId: number,
                public typeConnecteur: string,
                public puissance: number,
                public adresse: Adresse ){}
}

export class Adresse{
    constructor(public adresseId: number,
                public nomRue: string,
                public codePostal: string,
                public numCivique: number,
                public ville: string,
                public province: string){}
}