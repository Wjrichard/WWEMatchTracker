<script setup lang="ts">
import eventsStore,{_Events} from "@/stores/events.ts";
import ComboBox from "@/components/Layout/ComboBox.vue";
import {ref} from 'vue'
import {FileUtilities} from "@/utilities/file.ts";

const selectedEvent = ref(Event)

</script>

<template>
    <div class="overflow-auto pt-12 w-11/12 justify-center bg-white">
        <div class="sticky top-0 z-20">
            <span class="text-8xl flex w-full justify-center pb-10 sticky top-0">
                Events
            </span>
            <div class="w-full h-24 flex items-center sticky top-0 sticky-top z-10 border-b border-gray-200">
              <span class="w-full z-40 flex items-center justify-center">
                  <ComboBox
                      placeholder="Search for an event"
                      class="p-2 border border-gray-300 text-2xl z-50 w-2/3 flex justify-center"
                      :options="_Events"
                      :option-context="{
                          key: 'eventId',
                          displayKey: 'eventName',
                          valueKey: 'eventId',
                      }"
                      v-model="selectedEvent"
                  />
              </span>
            </div>
        </div>
        
        <ul role="list" class="divide-y divide-gray-200 h-full">
            <li v-for="e in _Events" :key="e.eventId" class="px-6 py-4">
                <div class="flex w-full">
                    <span class="w-1/3 flex items-center justify-center rounded-md">
                        <img class="h-48" :src="`data:image/jpeg;base64,${e.eventImage}`" alt="Image" />
                       
                    </span>
                    <div class="w-2/3 content-center">
                        <span class="flex w-full justify-center py-4 text-3xl font-extrabold">
                                {{e.eventName}}
                        </span>
                        <span class="flex w-full justify-center py-4 text-xl font-extrabold">
                                {{e.eventDate}}
                        </span>
                    </div>
                </div>
            </li>
        </ul>
    </div>
</template>
