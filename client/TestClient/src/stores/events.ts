import type { Event } from '@/types/event.ts'
import {ref} from 'vue'
import axios from "axios";
import { FileUtilities } from '@/utilities/file'

export const initializedEvent = <Event>{
    eventId: 0,
    eventName:'',
    eventDate:'',
}

export const _Events = ref<Event[]>([]);
export const selectedEvent = ref<Event>(initializedEvent);


export async function createEvent(event:Event){
    const newEvent = initializedEvent
    newEvent.eventName = event.eventName
    newEvent.eventImage = event.eventImage
    newEvent.eventDate = event.eventDate
    const response = await axios.post('https://localhost:44328/Events/AddEvent',newEvent)
}

export async function loadEvents() {
    try {
        const response = await axios.get('https://localhost:44328/Events/GetEvents');
        _Events.value = response.data/*.map(event => {
                return <Event>{
                    eventId: event.eventId,
                    eventName: event.eventName,
                    eventImage: event.eventImage,
                    eventDate: event.eventDate
                };
            });*/
        console.log(_Events.value)
    } catch (error) {
        console.error('Error fetching users:', error);
    }
}


export async function setEvent(eventId:number) {
    const curUser = _Events.value.find(event => event.eventId === eventId)
    if (curUser){
        selectedEvent.value = curUser
    }
}

const eventStore = { _Events, selectedEvent, loadEvents, initializedEvent, setEvent };
export default eventStore;
