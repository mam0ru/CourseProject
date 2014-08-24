$ ->
    $(document).ready () ->
        $('#inputTags').textext({
            plugins: 'tags autocomplete ajax prompt',
            prompt  : 'Введите теги...',
            ajax: {
            url: '/Exercise/TagAutocompliteSearch',
            dataType: 'json',
            cacheResults: true
            }
            })
        $('#inputAnswers').textext({
            plugins: 'tags prompt',
            prompt  : 'Введите ответы...'
            })