(function() {
  $(function() {
    return $(document).ready(function() {
      $('#inputTags').textext({
        plugins: 'tags autocomplete ajax prompt',
        prompt: 'Введите теги...',
        ajax: {
          url: '/Exercise/TagAutocompliteSearch',
          dataType: 'json',
          cacheResults: true
        }
      });
      return $('#inputAnswers').textext({
        plugins: 'tags prompt',
        prompt: 'Введите ответы...'
      });
    });
  });

}).call(this);

//# sourceMappingURL=TagAndAnswerInputSetting.js.map
