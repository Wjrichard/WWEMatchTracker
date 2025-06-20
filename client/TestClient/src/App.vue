<script setup lang="ts">
import SidebarLayout from "@/components/Layouts/SidebarLayout.vue";
import { useRoute } from "vue-router";
import UserAccount from "@/components/UserAccount.vue";
import eventStore from '@/stores/events';
import userStore from '@/stores/user';
import { computed, onBeforeMount } from 'vue';
import { format, parseISO, isWithinInterval, subDays, addDays, parse, isValid } from 'date-fns';

const route = useRoute();

onBeforeMount(async () => {
  await eventStore.loadEvents();
});
function parseEventDate(dateStr: string) {
  let date = parseISO(dateStr);
  if (isValid(date)) return date;
  date = parse(dateStr, 'MM/dd/yyyy', new Date());
  if (isValid(date)) return date;
  date = parse(dateStr, 'yyyy-MM-dd HH:mm:ss', new Date());
  if (isValid(date)) return date;
  date = new Date(dateStr);
  if (isValid(date)) return date;
  return null;
}
function parseEventDateRange(dateStr: string) {
  // Handle range: "MM/dd/yyyy - MM/dd/yyyy" or similar
  if (dateStr.includes('-')) {
    const parts = dateStr.split('-').map(s => s.trim());
    if (parts.length === 2) {
      const start = parseEventDate(parts[0]);
      const end = parseEventDate(parts[1]);
      if (start && end) return { start, end };
    }
  }
  // Single date
  const single = parseEventDate(dateStr);
  if (single) return { start: single, end: single };
  return null;
}

const getUpcomingEventLinks = computed(() => {
  const now = new Date();
  const windowStart = subDays(now, 100);
  const windowEnd = addDays(now, 30);
  return eventStore._EventDetails.value
    .filter(detail => {
      if (!detail.event.eventDate) return false;
      const range = parseEventDateRange(detail.event.eventDate);
      if (!range) return false;
      // Overlap: event ends after window starts AND event starts before window ends
      return range.end >= windowStart && range.start <= windowEnd;
    })
    .map(detail => {
      const range = parseEventDateRange(detail.event.eventDate);
      let dateLabel = detail.event.eventDate;
      if (range) {
        if (range.start.getTime() === range.end.getTime()) {
          dateLabel = format(range.start, 'yyyy-MM-dd');
        } else {
          dateLabel = `${format(range.start, 'yyyy-MM-dd')} - ${format(range.end, 'yyyy-MM-dd')}`;
        }
      }
      return {
        label: `${detail.event.eventName}`,
        route: { path: `/events/${detail.event.eventId}/matches` },
      };
    });
});

const sidebarItems = computed(() => {
  const items = [
    {
      icon: "icon-user",
      label: "Sign In",
      route: { path: "/" },
    },
  ];
  if (userStore.selectedUser.value && userStore.selectedUser.value.username) {
    items.push({
      icon: "icon-list",
      label: "Events",
      route: { path: "/events" },
    });
  }
  const groups = [
    { items },
  ];
  if (userStore.selectedUser.value && userStore.selectedUser.value.username) {
    groups.push({
      label: "Upcoming Events",
      items: getUpcomingEventLinks.value,
    });
  }
  return groups;
});
</script>

<template>
    <SidebarLayout
        :navigation-entries="sidebarItems"
        :currentRoutePath="route.path"
        class="h-screen"
    >
        <template #sidebarHeader>
                <h1 class="text-2xl font-bold">
                    WWE Predictions
                </h1>
        </template>

        <template #sidebarFooter>
            <UserAccount />
        </template>
        <RouterView />
    </SidebarLayout>
</template>