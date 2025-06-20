<script setup lang="ts">
import eventStore,{_EventDetails} from "@/stores/events.ts";
import type {EventDetails} from "@/types/eventDetails.ts";
import ComboBox from "@/components/Forms/Combobox.vue";
import Button from "@/components/General/Button.vue";
import {ref, computed, onBeforeMount, watch} from 'vue'
import {useRouter,useRoute} from "vue-router";
import { PlusCircleIcon } from "@heroicons/vue/24/solid";

const $router = useRouter();
const selectedEvent = ref<EventDetails>()
const route = useRoute();

async function openEventDetails(eventId: number) {
    eventStore.setEvent(eventId)
    await $router.push({ name: 'matches', params: { eventId: eventId } });
}

const eventOptions = computed(() =>
    _EventDetails.value.map(detail => detail.event)
);

const searchTerm = ref("");

const filteredEventOptions = computed(() => {
  if (!searchTerm.value) return eventOptions.value;
  console.log(searchTerm.value)
  return eventOptions.value.filter(event =>
    event.eventName.toLowerCase().includes(searchTerm.value.toLowerCase())
  );
});

const filteredEventDetails = computed(() => {
  if (selectedEvent.value) {
    return _EventDetails.value.filter(detail => detail.event.eventId === selectedEvent.value.eventId);
  }
  if (!searchTerm.value) return _EventDetails.value;
  return _EventDetails.value.filter(detail =>
    detail.event.eventName.toLowerCase().includes(searchTerm.value.toLowerCase())
  );
});

function onComboBoxSelect(event: any) {
  selectedEvent.value = event;
}

const newEvents = ref([
  // Example: { eventName: '', eventDate: '', eventImage: '' }
]);

function addNewEvent() {
  newEvents.value.push({ eventName: '', eventDate: '', eventImage: '' });
}

function saveNewEvent(idx: number) {
  // TODO: Implement actual save logic (e.g., API call)
  // For now, just remove from newEvents
  newEvents.value.splice(idx, 1);
}

function cancelNewEvent(idx: number) {
  newEvents.value.splice(idx, 1);
}

watch(searchTerm, (newValue) => {
  console.log('searchTerm changed:', newValue);
});

onBeforeMount(async () => {
  await eventStore.loadEvents();
});
</script>

<template>
  <div class="flex flex-col h-full w-full bg-white">
    <div class="sticky top-0 z-10 bg-white">
      <div class="relative w-full">
        <Button
          @click="$router.push('/')"
          icon="icon-arrow-left"
          label="Back"
          color="secondary"
          class="absolute left-4 top-4"
        />
      </div>
      <h1 class="w-full text-center flex items-center justify-center py-6 text-6xl font-bold border-b border-gray-200">
        Events
      </h1>
      <div class="w-full py-3 flex items-center border-b border-gray-200">
        <span class="w-full z-40 flex h-full items-center justify-center">
          <ComboBox
            placeholder="Search for an Event"
            class="p-2 border border-gray-300 text-2xl z-50 w-2/3 flex justify-center"
            :options="filteredEventOptions"
            :option-context="{
              key: 'eventId',
              displayKey: 'eventName',
              valueKey: 'eventId',
            }"
            v-model="selectedEvent"
            :search-term="searchTerm"
            @update:search-term="searchTerm = $event"
            @update:model-value="onComboBoxSelect"
          />
        </span>
      </div>
      <span class="sticky top-0 z-20 bg-white flex w-full justify-center py-3 shadow-md border-b border-gray-200 cursor-pointer hover:bg-gray-100" @click="addNewEvent">
        <PlusCircleIcon class="h-12 w-12" />
      </span>
    </div>
    <div class="overflow-auto w-full flex-1 bg-white truncate">
      <ul role="list" class="divide-y divide-gray-200 h-full">
        <!-- New Event Row: sticky just below the plus icon if present -->
        <li v-if="newEvents.length > 0" class="z-20 p-4 bg-yellow-50 flex w-full items-center gap-4 shadow-md">
          <Input v-model="newEvents[0].eventName" 
                 placeholder="Event Name" 
                 class="w-1/3 border rounded-lg p-2 border-zinc-500"
          />
          <Input v-model="newEvents[0].eventDate" 
                 placeholder="Event Date" 
                 class="w-1/3 border rounded-lg p-2 border-zinc-500"
          />
          <div class="flex gap-2 ml-auto w-1/3">
            <button @click.stop="saveNewEvent(0)" class="rounded bg-green-100 hover:bg-green-200 text-green-700 hover:text-green-900 p-2 flex items-center justify-center">
              <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 13l4 4L19 7" />
              </svg>
            </button>
            <button @click.stop="cancelNewEvent(0)" class="rounded bg-red-100 hover:bg-red-200 text-red-700 hover:text-red-900 p-2 flex items-center justify-center">
              <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
              </svg>
            </button>
          </div>
        </li>
        <!-- Existing event list -->
        <li v-for="detail in filteredEventDetails" :key="detail.event.eventId" class="py-4 hover:bg-gray-50 cursor-pointer"
        @click="openEventDetails(detail.event.eventId)">
          <div class="flex w-full">
            <span class="w-1/3 flex items-center justify-center rounded-md">
              <img class="h-48" :src="`data:image/jpeg;base64,${detail.event.eventImage}`" alt="Image" />
            </span>
            <div class="w-2/3 content-center">
              <span class="flex w-full justify-center py-4 text-3xl font-extrabold">
                {{detail.event.eventName}}
              </span>
              <span class="flex w-full justify-center py-4 text-xl font-extrabold">
                {{detail.event.eventDate}}
              </span>
            </div>
          </div>
        </li>
      </ul>
    </div>
  </div>
</template>
