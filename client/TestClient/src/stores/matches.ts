import type { Match } from '@/types/match.ts'
import {ref} from 'vue'
import axios from "axios";

export const initializedMatch = {
    matchId: 0,
    matchName:''
}

export const _Matches = ref<Match[]>([]);
export const selectedMatch = ref<Match>(initializedMatch);


export async function createMatch(match:Match){
    const newMatch = initializedMatch
    newMatch.matchName = match.matchName
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
