import type { Event } from '@/types/event.ts'
import {ref} from 'vue'
import axios from "axios";
import type {EventDetails} from "@/types/eventDetails.ts";

export const initializedEvent = <EventDetails>{
    event: {
        eventId: 0,
        eventName:'',
        eventDate:'',
        eventImage:''
    },
    matches: []
}

export const _EventDetails = ref<EventDetails[]>([]);
export const selectedEvent = ref<EventDetails>(initializedEvent);


export async function createEvent(event:Event){
    const newEvent = initializedEvent
    newEvent.event.eventName = event.eventName
    newEvent.event.eventImage = event.eventImage
    newEvent.event.eventDate = event.eventDate
    const response = await axios.post('https://localhost:44328/Events/AddEvent',newEvent)
}

export async function loadEvents() {
    try {
        const response = await axios.get('https://localhost:44328/Events/GetEventDetails');
        _EventDetails.value = response.data
        console.log(_EventDetails.value)
    } catch (error) {
        console.error('Error fetching users:', error);
    }
}


export function setEvent(eventId:number) {
    const curEvent = _EventDetails.value.find(detail => detail.event.eventId === eventId)
    if (curEvent){
        selectedEvent.value = curEvent
    }
}

const eventStore = { _EventDetails, selectedEvent, loadEvents, initializedEvent, setEvent, createEvent };
export default eventStore;
