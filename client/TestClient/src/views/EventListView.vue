﻿<script setup lang="ts">
import eventStore,{_EventDetails} from "@/stores/events.ts";
import type {EventDetails} from "@/types/eventDetails.ts";
import ComboBox from "@/components/Layout/ComboBox.vue";
import {ref} from 'vue'
import {useRouter,useRoute} from "vue-router";

const $router = useRouter();
const selectedEvent = ref<EventDetails>()
const route = useRoute();

async function openEventDetails(eventId: number) {
    eventStore.setEvent(eventId)
    await $router.push({ name: 'matches', params: { eventId: eventId } });
}

</script>

<template>
    <div class="sticky top-0 z-10 bg-white">
        <h1 class="w-full text-center flex items-center justify-center py-12 text-8xl font-bold">
            Events
        </h1>
        <div class="sticky top-0 z-20">
            <div class="w-full h-24 flex items-center sticky top-0 sticky-top z-10 border-b border-gray-200">
              <span class="w-full z-40 flex h-full pt-4 items-center justify-center">
                  <ComboBox
                      placeholder="Search for an event"
                      class="p-2 border border-gray-300 text-2xl z-50 w-2/3 flex justify-center"
                      :options="_EventDetails"
                      :option-context="{
                          key: 'event.eventId',
                          displayKey: 'event.eventName',
                          valueKey: 'event.eventId',
                      }"
                      v-model="selectedEvent"
                  />
              </span>
            </div>
        </div>
    </div>
    <div class="overflow-auto pt-12 w-full justify-center bg-white truncate">
        
        <ul role="list" class="divide-y divide-gray-200 h-full">
            <li v-for="detail in _EventDetails" :key="detail.event.eventId" class="px-6 py-4 hover:bg-gray-50 cursor-pointer"
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
</template>
