﻿
<script
    setup
    lang="ts"
    generic="
    T extends ReadonlyArray<unknown> | Record<string, ReadonlyArray<unknown>>
  "
>
import {
    type AcceptableValue,
    ComboboxAnchor,
    ComboboxContent,
    ComboboxEmpty,
    ComboboxGroup,
    ComboboxInput,
    ComboboxItem,
    ComboboxItemIndicator,
    ComboboxLabel,
    ComboboxPortal,
    ComboboxRoot,
    type ComboboxRootProps,
    ComboboxSeparator,
    ComboboxTrigger,
    ComboboxViewport,
} from "reka-ui";
import { computed } from "vue";
import { twMerge } from "tailwind-merge";
import {CheckIcon,ChevronUpDownIcon} from "@heroicons/vue/24/solid";
import {
    defaultRingColorClasses,
    defaultSeparatorStyles,
} from "@/components/Styles/DefaultGenerators.ts";
import { tw } from "@/components/utilities/tw.ts";

export interface RekaComboBoxProps<T> {
    options?: T;
    placeholder?: string;
    groupContext?: { key: string; displayKey: string; valueKey: string };
    optionContext?: { key: string; displayKey: string; valueKey: string };
}

const model = defineModel<AcceptableValue>();

const props = withDefaults(
    defineProps<RekaComboBoxProps<T> & Partial<ComboboxRootProps>>(),
    {
        open: undefined,
        disabled: undefined,
        resetSearchTermOnBlur: undefined,
        resetSearchTermOnSelect: undefined,
    },
);

const comboboxRootProps = computed(() =>
    Object.fromEntries(Object.entries(props).filter(([_, v]) => v !== undefined)),
);

const ComboboxItemStyles = tw`flex cursor-pointer select-none items-center justify-between rounded-md p-2 text-base data-[highlighted]:!text-white data-[highlighted]:bg-blue-500 data-[state=checked]:font-semibold data-[highlighted]:outline-none`;
const ComboboxItemIndicatorStyles = tw`inline-flex items-center justify-center`;
</script>

<template>
    <!-- https://github.com/unovue/reka-ui/issues/1702 - Combobox allow showing options on focus -->
    <ComboboxRoot
        v-bind="comboboxRootProps"
        v-model="model"
        :class="
      twMerge(
        defaultRingColorClasses(),
        tw`rounded-md p-1 focus-within:ring-2 focus-within:ring-blue-500`,
      )
    "
    >
        <ComboboxAnchor
            :class="twMerge(tw`flex w-full items-center justify-between rounded-md`)"
            ref="comboboxAnchorRef"
        >
            <ComboboxInput
                :display-value="
          (v) => {
            if (v) {
              if (optionContext) {
                return v[optionContext.displayKey];
              } else {
                return v;
              }
            }
          }
        "
                :class="twMerge(tw`h-full w-full px-2 outline-none`)"
                :placeholder="placeholder"
            />
            <ComboboxTrigger
                :class="
          twMerge(tw`cursor-pointer px-2 text-zinc-500 hover:text-zinc-800`)
        "
            >
                <div>
                    <ChevronUpDownIcon class="my-1 size-5" />
                </div>
            </ComboboxTrigger>
        </ComboboxAnchor>
        <ComboboxPortal>
            <ComboboxContent
                :class="
          twMerge(
            defaultRingColorClasses(),
            tw`z-10 mt-2 max-h-60 w-(--reka-combobox-trigger-width) overflow-auto rounded-md bg-white py-1 shadow-lg`,
          )
        "
                position="popper"
            >
                <ComboboxViewport :class="twMerge(tw`p-[5px]`)">
                    <ComboboxEmpty :class="twMerge(tw`py-2 text-center font-medium`)">
                        No options found
                    </ComboboxEmpty>
                    <template
                        v-if="groupContext"
                        v-for="(group, index) in options"
                        :key="group[groupContext.key]"
                    >
                        <ComboboxGroup>
                            <ComboboxSeparator
                                v-if="index !== 0"
                                :class="twMerge(defaultSeparatorStyles())"
                            />

                            <ComboboxLabel
                                :class="
                  twMerge(
                    tw`flex items-center justify-between text-base text-blue-500`,
                  )
                "
                            >
                                {{ group[groupContext.displayKey] }}
                            </ComboboxLabel>

                            <ComboboxItem
                                v-for="option in group[groupContext.valueKey]"
                                :key="optionContext ? option[optionContext.key] : option"
                                :value="option"
                                :class="twMerge(ComboboxItemStyles)"
                            >
                <span>
                  {{
                        optionContext ? option[optionContext.displayKey] : option
                    }}
                </span>
                                <ComboboxItemIndicator
                                    :class="twMerge(ComboboxItemIndicatorStyles)"
                                >
                                    <CheckIcon class="size-4" />
                                </ComboboxItemIndicator>
                            </ComboboxItem>
                        </ComboboxGroup>
                    </template>

                    <template
                        v-if="!groupContext"
                        v-for="option in options"
                        :key="optionContext ? option[optionContext.key] : option"
                    >
                        <ComboboxItem
                            :value="option as AcceptableValue"
                            :class="twMerge(ComboboxItemStyles)"
                        >
              <span>
                {{ optionContext ? option[optionContext.displayKey] : option }}
              </span>
                            <ComboboxItemIndicator
                                :class="twMerge(ComboboxItemIndicatorStyles)"
                            >
                                <CheckIcon />
                            </ComboboxItemIndicator>
                        </ComboboxItem>
                    </template>
                </ComboboxViewport>
            </ComboboxContent>
        </ComboboxPortal>
    </ComboboxRoot>
</template>