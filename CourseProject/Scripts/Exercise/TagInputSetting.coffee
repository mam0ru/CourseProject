$ ->
    $(document).ready () ->
        $('#inputTags').textext({
            plugins: 'tags autocomplete ajax prompt',
            prompt  : 'Write here youre tags...',
            ajax: {
            url: '/Exercise/TagAutocompliteSearch',
            dataType: 'json',
            cacheResults: true
            }
            })
        $('#inputAnswers').textext({
            plugins: 'tags prompt',
            prompt  : 'Write here right answers...'
            })