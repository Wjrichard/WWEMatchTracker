import type { Match } from '@/types/match.ts'
import {ref} from 'vue'
import axios from "axios";

export const initializedMatch = {
    matchId: 0,
    shortName: '',
    team1:'',
    team2:'',
    team3:'',
    team4:'',
    team5:'',
    team6:'',
    team7:'',
    team8:'',
}

export const _Matches = ref<Match[]>([]);
export const selectedMatch = ref<Match>(initializedMatch);


export async function createMatch(match:Match){
    const newMatch = initializedMatch
    newMatch.shortName = match.shortName
    newMatch.team1 = match.team1
    newMatch.team2 = match.team2
    newMatch.team3 = match.team3 ?? ''
    newMatch.team4 = match.team4 ?? ''
    newMatch.team5 = match.team5 ?? ''
    newMatch.team6 = match.team6 ?? ''
    newMatch.team7 = match.team7 ?? ''
    newMatch.team8 = match.team8 ?? ''
    const response = await axios.post('https://localhost:44328/Match/AddMatch',newMatch)
}

export async function loadMatches() {
    try {
        const response = await axios.get('https://localhost:44328/Match/GetMatches');
        _Matches.value = response.data;
    } catch (error) {
        console.error('Error fetching users:', error);
    }
}


export async function setMatch(matchId:number) {
    const curUser = _Matches.value.find(match => match.matchId === matchId)
    if (curUser){
        selectedMatch.value = curUser
    }
}

const matchesStore = { _Matches, selectedMatch, loadMatches, initializedMatch, setMatch };
export default matchesStore;
