import type { Match } from '@/types/match.ts'
import type { EventDetails} from "@/types/eventDetails.ts";
import type { Team } from '@/types/team.ts'
import {ref} from 'vue'
import axios from "axios";

export const initializedMatch = {
    matchId: 0,
    matchName:''
}

export const _Matches = ref<EventDetails>();
export const _CurrentMatchTeams = ref<Team[]>([])
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

export async function loadMatchDetails(matchId:number) {
    try {
        const response = await axios.get('https://localhost:44328/Match/GetMatches');
        _Matches.value = response.data;
    } catch (error) {
        console.error('Error fetching users:', error);
    }
}

export async function loadMatchParticipants(matchId: number) {
    try {
        const response = await axios.get(`https://localhost:44328/Match/${matchId}/GetMatchParticipants`);
        _CurrentMatchTeams.value = response.data;
        console.log(_CurrentMatchTeams);
    } catch (error) {
        console.error('Error fetching users:', error);
    }
}

export async function setMatch(matchId:number) {
    const curUser = _Matches.value?.matches.find(match => match.matchId === matchId)
    if (curUser){
        selectedMatch.value = curUser
    }
}

const matchesStore = { _Matches, selectedMatch, loadMatches, initializedMatch, setMatch, _CurrentMatchTeams, loadMatchParticipants };
export default matchesStore;
