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
        tags = []
        tagsElement = $("[name='currTag']")
        $.each tagsElement, (e,v) ->
            tags.push(v.value)
        $("#inputTags").textext()[0].tags().addTags(tags)        
        answers = []
        answersElement = $("[name='currAnswer']")
        $.each answersElement, (e,v) ->
            answers.push(v.value)    
        $("#inputAnswers").textext()[0].tags().addTags(answers)