export interface BorneDTO {
    borneId: number;
    typeConnecteur: string;
    puissance: number;
    numCivique: number;
    nomRue: string;
    ville: string;
    province: string;
    codePostal: string;
    favorisId: number;
    moyenneNote?: number | null;
  }
