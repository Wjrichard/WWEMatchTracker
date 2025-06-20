<script setup lang="ts">
import {
  AccordionContent,
  AccordionHeader,
  AccordionItem,
  AccordionRoot,
  AccordionTrigger,
} from "reka-ui";
import { defaultRingColorClasses } from "../Styles/DefaultGenerators";
import { twMerge } from "tailwind-merge";
import Icon from "./Icon.vue";

export interface AccordionItem {
  /** The header of the accordion item */
  header: string;
  /** The content of the accordion item */
  content?: string;
  data?: Record<string, any>;
}

export interface AccordionProps {
  items: AccordionItem[];
  multiple?: boolean;
  /** Default item to have open. Matches against the `AccordionItem.title` node */
  defaultItem?: string;
  horizontal?: boolean;
  borderless?: boolean;
}

defineProps<AccordionProps>();
</script>

<template>
  <AccordionRoot
    collapsible
    :type="multiple ? 'multiple' : 'single'"
    :orientation="horizontal ? 'horizontal' : 'vertical'"
    :data-orientation="horizontal ? 'horizontal' : 'vertical'"
    :default-value="defaultItem"
    :class="
      twMerge(
        'flex h-fit data-[orientation=horizontal]:flex-row data-[orientation=vertical]:flex-col',
        'rounded p-4 lg:rounded-lg',
        'overflow-x-auto',
        borderless ? 'p-0' : defaultRingColorClasses(),
      )
    "
  >
    <AccordionItem v-for="item in items" :value="item.header" class="pl-0.5">
      <AccordionHeader
        class="cursor-pointer rounded p-2 whitespace-nowrap hover:bg-zinc-100"
      >
        <AccordionTrigger
          class="group flex w-full cursor-pointer items-center justify-between gap-2"
        >
          <span class="text-lg font-medium">{{ item.header }}</span>
          <div class="duration-300 group-data-[state=open]:rotate-180">
            <Icon name="icon-chevron-down" />
          </div>
        </AccordionTrigger>
      </AccordionHeader>
      <AccordionContent
        class="border-l border-transparent p-4 data-[orientation=horizontal]:p-2 data-[state=open]:data-[orientation=vertical]:border-zinc-200"
      >
        <slot name="content" v-bind="item">
          {{ item.content }}
        </slot>
      </AccordionContent>
    </AccordionItem>
  </AccordionRoot>
</template>
