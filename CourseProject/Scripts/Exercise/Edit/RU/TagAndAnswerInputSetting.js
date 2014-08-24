(function() {
  $(function() {
    return $(document).ready(function() {
      var answers, answersElement, tags, tagsElement;
      $('#inputTags').textext({
        plugins: 'tags autocomplete ajax prompt',
        prompt: 'Введите теги...',
        ajax: {
          url: '/Exercise/TagAutocompliteSearch',
          dataType: 'json',
          cacheResults: true
        }
      });
      $('#inputAnswers').textext({
        plugins: 'tags prompt',
        prompt: 'Введите ответы...'
      });
      tags = [];
      tagsElement = $("[name='currTag']");
      $.each(tagsElement, function(e, v) {
        return tags.push(v.value);
      });
      $("#inputTags").textext()[0].tags().addTags(tags);
      answers = [];
      answersElement = $("[name='currAnswer']");
      $.each(answersElement, function(e, v) {
        return answers.push(v.value);
      });
      return $("#inputAnswers").textext()[0].tags().addTags(answers);
    });
  });

}).call(this);

//# sourceMappingURL=TagAndAnswerInputSetting.js.map
